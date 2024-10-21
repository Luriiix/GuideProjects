namespace GuideProjects;

public class HuntingTheManticore {
	private int _manticoreMaxHealth;
	private int _manticoreHealth;
	private int _cityMaxHealth;
	private int _cityHealth;
	private int _roundNumber;

	public void Main() {
		_cityMaxHealth = 15;
		_manticoreMaxHealth = 10;
		_cityHealth = _cityMaxHealth;
		_manticoreHealth = _manticoreMaxHealth;
		_roundNumber = 0;


		var manticorePosition = PositionGetter("Where should the manticore be ");
		Console.Clear();

		while (true) {
			_roundNumber += 1;
			var cannonDamage = CannonDamage(_roundNumber);
			StatusWriter();
			Console.WriteLine($"The Cannon is expected to deal {cannonDamage} this round!");
			var cityGuess = PositionGetter("Where should the cannon shoot ");

			if (cityGuess == manticorePosition) {
				Console.WriteLine("WP you hit it! \n");
				_manticoreHealth -= cannonDamage;
				if (_manticoreHealth <= 0) {
					_manticoreHealth = 0;
					break;
				}
			}

			if (cityGuess > manticorePosition) Console.WriteLine("Too far! \n");
			if (cityGuess < manticorePosition) Console.WriteLine("Too close! \n");

			_cityHealth -= 1;
			if (_cityHealth == 0) break;
		}

		StatusWriter();
		Console.WriteLine(
			_cityHealth == 0 ? "The City has no HP anymore, you lost!" : "You killed the Manticore, you won!"
		);

		return;

		void StatusWriter() {
			Console.WriteLine(
				$"STATUS: Round: {_roundNumber} City: {_cityHealth}/{_cityMaxHealth} Manticore: {_manticoreHealth}/{_manticoreMaxHealth}"
			);
		}

		int PositionGetter(string question) {
			int input;
			do {
				Console.Write($"{question}(1-100): ");
				input = Convert.ToInt32(Console.ReadLine());
				if (input is < 1 or > 100) Console.WriteLine("It must be between 1 and 100! Try again!");
			} while (input is < 1 or > 100);

			return input;
		}

		int CannonDamage(int round) {
			if (round % 3 == 0 && round % 5 == 0) return 10;
			if (round % 3 == 0 || round % 5 == 0) return 3;
			return 1;
		}
	}
}
