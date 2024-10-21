using GuideProjects;

var projects = new Dictionary<string, Action>();

AddProject<Exit>();
AddProject<Puzzle>();
AddProject<Hangman>();
AddProject<HuntingTheManticore>();
AddProject<RockPaperScissors>();
AddProject<TicTacToe>();

Console.WriteLine("Welcome to some of my Guide-Projects");
Console.WriteLine("These Projects are from Tasks of 'The C# Players Guide' Book");
while (true) {
	Console.WriteLine("Which Project would you like to start?");
	
	for (var i = 0; i < projects.Count; i++) {
		Console.WriteLine(i + 1 + " - " + projects.Keys.ElementAt(i));
	}
	Console.Write("Enter your choice (Number or Name): ");
	var input = Console.ReadLine();

	if (projects.TryGetValue(input, out var value)) {
        value();
        continue;
	}

	if (int.TryParse(input, out var number) && number >= 1 && number <= projects.Count) {
		projects.ToList()[number - 1].Value();
		continue;
	}
	
	Console.WriteLine("\nPlease enter a valid number or name.\n");
}

void AddProject<T>() {
	var type = typeof(T);
	var classConstructor = type.GetConstructor(Type.EmptyTypes);
	var classObject = classConstructor.Invoke([]);
	var methodInfo = type.GetMethods().First();

	var name = type.Name;
	projects.Add(name, () => methodInfo.Invoke(classObject, []));
}

public class Exit {
	public void Main() {
		Environment.Exit(0);
	}
}