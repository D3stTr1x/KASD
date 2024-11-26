using System.Text.RegularExpressions;
using MyHashMap;

public enum VariableType
{
    Int,
    Float,
    Double
}

public class Program
{
    static void Main()
    {

        MyHashMap<string, (VariableType Type, string Value)> variableDefinitions = new MyHashMap<string, (VariableType, string)>();
        string fileContent = File.ReadAllText("input.txt");
        string pattern = @"\s*(int|float|double)\s+([a-zA-Z_][a-zA-Z0-9_]*)\s*=\s*(\d+)\s*;";
        MatchCollection matches = Regex.Matches(fileContent, pattern);

        foreach (Match match in matches)
        {
            string type = match.Groups[1].Value.ToLower(); 
            string variableName = match.Groups[2].Value;   
            string value = match.Groups[3].Value;          
            VariableType varType;
            switch (type)
            {
                case "int":
                    varType = VariableType.Int;
                    break;
                case "float":
                    varType = VariableType.Float;
                    break;
                case "double":
                    varType = VariableType.Double;
                    break;
                default:
                    continue;
            }
            if (variableDefinitions.ContainsKey(variableName))
            {
                Console.WriteLine($"Переопределение переменной: {variableName}");
                continue; 
            }

            variableDefinitions.Put(variableName, (varType, value));
        }

        string[] allLines = File.ReadAllLines("input.txt");
        List<string> definitions = new List<string>();
        string currentDefinition = "";
        foreach (string line in allLines)
        {
            string trimmedLine = line.Trim();
            if (string.IsNullOrEmpty(trimmedLine))
                continue;

            currentDefinition += " " + trimmedLine;
            if (trimmedLine.EndsWith(";"))
            {
                definitions.Add(currentDefinition.Trim());
                currentDefinition = "";
            }
        }

        foreach (string definition in definitions)
        {
            if (!Regex.IsMatch(definition, pattern, RegexOptions.IgnoreCase)) // RegexOptions.IgnoreCase указывает, что регистр букв следует игнорировать.
            {
                Console.WriteLine($"Некорректный тип: {definition}");
            }
        }


        using (StreamWriter writer = new StreamWriter("output.txt"))
        {
            foreach (var entry in variableDefinitions.EntrySet())
            {
                string typeName = entry.Value.Type.ToString().ToLower();
                writer.WriteLine($"{typeName} => {entry.Key}({entry.Value.Value})");
            }
        }
    }
}