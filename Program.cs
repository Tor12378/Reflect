using System.Reflection;



namespace Reflection
{

    public class Program
    {
        static void Main()
        {
            var company = new Company(100)
            {
                PublicEmployeeCount = 200,
                Name = "OpenAI",
                Type = "Technology",
                CompanyType = "Software",
            };
            GetAllInfoAboutClass(company);
        }

        public static void GetAllInfoAboutClass<T>(T obj)
        {
            var type = typeof(T);

            GetAllFields(obj);
            GetAllProperties(obj);
            InvokeAllMethods(obj);
            GetAllConstructors(obj);
        }

        public static void GetAllConstructors<T>(T obj)
        {
            var type = typeof(T);
            Console.WriteLine("Constructors");
            while (type != null)
            {
                var constructors = type.GetConstructors(BindingFlags.Public
                                                        | BindingFlags.Instance | BindingFlags.NonPublic);
                Console.WriteLine($"Base Type: {type.BaseType}");
                foreach (var constructorInfo in constructors)
                    Console.WriteLine($"Name: {constructorInfo.Name} MembersType: {constructorInfo.MemberType}");
                type = type.BaseType;
            }
        }

        public static void InvokeAllMethods<T>(T obj)
        {
            var type = typeof(T);
            Console.WriteLine("Methods");
            while (type != null)
            {
                var methods = type.GetMethods(BindingFlags.Public |
                                              BindingFlags.Static | BindingFlags.Instance
                                              | BindingFlags.NonPublic);
                foreach (var methodInfo in methods)
                {
                    var parameters = methodInfo.GetParameters();
                    if (parameters.Length > 0)
                    {
                        var arguments = new object[parameters.Length];
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            var parameterType = parameters[i].ParameterType;
                            arguments[i] = CreateValueForMethod(parameterType);
                        }

                        var result = methodInfo.Invoke(obj, arguments);
                        if (result != null)
                        {
                            Console.WriteLine(result);
                        }
                    }
                    else
                    {
                        Console.WriteLine(methodInfo.Invoke(obj, parameters: null));
                    }
                }

                type = type.BaseType;
            }
        }

        public static void GetAllProperties<T>(T obj)
        {
            var type = typeof(T);
            Console.WriteLine("Properties".PadLeft(10, '-'));
            while (type != null)
            {
                var properties = type
                    .GetProperties(BindingFlags.Public |
                                   BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (var propertyInfo in properties)
                {
                    Console.WriteLine($"Name: {propertyInfo.Name} | " +
                                      $"Value: {propertyInfo.GetValue(obj)}");
                    Console.WriteLine("Update Property");
                    var propType = propertyInfo.PropertyType;
                    var newVal = CreateValue(propType);
                    if (newVal == null)
                        propertyInfo.SetValue(obj, "Maybe something?");
                    else
                    {
                        propertyInfo.SetValue(obj, 10);
                    }

                    Console.WriteLine($"Name: {propertyInfo.Name} | " +
                                      $"Value: {propertyInfo.GetValue(obj)}");
                }

                type = type.BaseType;
            }
        }

        private static object CreateValueForMethod(Type type)
        {
            var rnd = new Random();
            if (type == typeof(int))
                return rnd.Next(-100, 100);
            else if (type == typeof(Array))
                return "hello";
            return null;
        }

        private static object CreateValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        public static void GetAllFields<T>(T obj)
        {
            var type = typeof(T);
            Console.WriteLine("Fields");
            while (type != null)
            {
                var fields = type.GetFields(BindingFlags.Public |
                                            BindingFlags.NonPublic | BindingFlags.Instance
                                            | BindingFlags.Static);

                foreach (var field in fields)
                {
                    Console.WriteLine($"Name: {field.Name} | " +
                                      $"Value: {field.GetValue(obj)}");
                    var fieldType = CreateValue(field.FieldType);
                    Console.WriteLine("Update Field");
                    if (fieldType == null)
                    {
                        field.SetValue(obj, "I am changed field");
                    }
                    else
                    {
                        field.SetValue(obj, -100);
                    }

                    Console.WriteLine($"Name: {field.Name} | " +
                                      $"Value: {field.GetValue(obj)}");
                }

                type = type.BaseType;
            }
        }
    }
}