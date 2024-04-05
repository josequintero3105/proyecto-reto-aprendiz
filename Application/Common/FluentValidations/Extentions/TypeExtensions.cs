using System.Linq.Expressions;
using System.Reflection;

namespace Application.Common.Validations.Extensions
{
    /// <summary>
    /// ObjectActivator
    /// </summary>
    /// <param name="args">The arguments.</param>
    /// <returns></returns>
    public delegate object ObjectActivator(params object[] args);

    /// <summary>
    /// TypeExtensions
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// News the specified arguments.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static object New(this Type input, params object[] args)
        {
            IEnumerable<Type> constructorTypes = args.Select(p => p.GetType());
            ConstructorInfo constructorInfo = input.GetConstructor(constructorTypes.ToArray())
                ?? input.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
                    .Single();
            ParameterInfo[] parametersInfo = constructorInfo?.GetParameters() ?? Array.Empty<ParameterInfo>();
            ParameterExpression parameterExpression = Expression.Parameter(typeof(object[]), "args");
            Expression[] argumentExpressions = new Expression[parametersInfo.Length];
            for (int index = 0; index < parametersInfo.Length; index++)
            {
                argumentExpressions[index] = NewArgumentExpression(parametersInfo[index], parameterExpression, index);
            }
            NewExpression newExpression = Expression.New(constructorInfo, argumentExpressions);
            LambdaExpression lambdaActivator = Expression.Lambda(typeof(ObjectActivator), newExpression, parameterExpression);
            ObjectActivator activator = (ObjectActivator)lambdaActivator.Compile();
            return activator(args);
        }
        private static Expression NewArgumentExpression(ParameterInfo parameterInfo,
            ParameterExpression parameterExpression, int index)
        {
            ConstantExpression indexExpression = Expression.Constant(index);
            Type parameterType = parameterInfo.ParameterType;
            BinaryExpression accessorExpression = Expression.ArrayIndex(parameterExpression, indexExpression);
            UnaryExpression castExpression = Expression.Convert(accessorExpression, parameterType);
            return castExpression;
        }
    }
}
