INSERT INTO IrregularVerbs (RussianUnitId, FirstFormId, SecondFormId, ThirdFormId, Comment) 
VALUES 
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'подниматься'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'arise' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'arose' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'arisen' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'пробуждать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'awake' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'awoke' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'awoken' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'быть'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'be' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'was/were' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'been' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'рожать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'bear' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'bore' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'born' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'бить'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'beat' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'beat' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'beaten' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'становиться'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'become' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'became' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'become' AND [eng].[Type] = 10), 
	'Кем то'
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'начинать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'begin' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'began' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'begun' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'наклонять'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'bend' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'bent' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'bent' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'заключать пари'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'bet' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'bet' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'bet' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'связывать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'bind' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'bound' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'bound' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'кусать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'bite' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'bit' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'bitten' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'кровоточить'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'bleed' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'bled' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'bled' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'дуть'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'blow' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'blew' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'blown' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'ломать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'break' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'broke' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'broken' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'разводить животных'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'breed' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'bred' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'bred' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'приносить'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'bring' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'brought' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'brought' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'строить'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'build' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'built' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'built' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'покупать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'buy' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'bought' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'bought' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'ловить'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'catch' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'caught' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'caught' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'выбирать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'choose' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'chose' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'chosen' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'цепляться'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'cling' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'clung' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'clung' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'приходить'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'come' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'came' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'come' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'стоить'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'cost' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'cost' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'cost' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'резать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'cut' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'cut' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'cut' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'вести дела'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'deal' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'dealt' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'dealt' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'копать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'dig' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'dug' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'dug' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'делать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'do' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'did' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'done' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'рисовать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'draw' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'drew' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'drawn' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'пить'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'drink' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'drank' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'drunk' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'водить автомобиль'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'drive' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'drove' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'driven' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'кушать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'eat' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'ate' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'eaten' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'падать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'fall' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'fell' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'fallen' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'кормить'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'feed' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'fed' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'fed' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'чувствовать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'feel' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'felt' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'felt' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'бороться'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'fight' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'fought' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'fought' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'находить'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'find' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'found' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'found' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'сбегать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'flee' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'fled' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'fled' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'летать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'fly' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'flew' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'flown' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'запрещать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'forbid' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'forbade' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'forbidden' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'забывать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'forget' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'forgot' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'forgotten' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'прощать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'forgive' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'forgave' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'forgiven' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'замораживать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'freeze' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'froze' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'frozen' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'получать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'get' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'got' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'got/gotten' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'давать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'give' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'gave' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'given' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'идти'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'go' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'went' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'gone' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'расти'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'grow' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'grew' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'grown' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'висеть'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'hang' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'hung' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'hung' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'иметь'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'have' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'had' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'had' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'слышать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'hear' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'heard' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'heard' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'прятать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'hide' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'hid' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'hidden' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'ударять'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'hit' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'hit' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'hit' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'держать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'hold' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'held' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'held' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'причинять боль'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'hurt' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'hurt' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'hurt' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'сохранять'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'keep' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'kept' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'kept' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'знать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'know' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'knew' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'known' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'класть'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'lay' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'laid' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'laid' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'лидировать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'lead' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'led' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'led' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'учиться'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'learn' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'learnt' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'learnt' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'покидать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'leave' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'left' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'left' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'давать взаймы'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'lend' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'lent' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'lent' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'позволять'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'let' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'let' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'let' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'лежать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'lie' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'lay' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'lain' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'освещать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'light' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'lit' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'lit' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'терять'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'lose' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'lost' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'lost' AND [eng].[Type] = 10), 
	NULL
)
,
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'мастерить'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'make' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'made' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'made' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'значить'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'mean' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'meant' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'meant' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'встречать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'meet' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'met' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'met' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'платить'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'pay' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'paid' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'paid' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'класть'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'put' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'put' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'put' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'читать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'read' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'read' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'read' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'ездить верхом'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'ride' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'rode' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'ridden' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'звонить'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'ring' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'rang' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'rung' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'возрастать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'rise' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'rose' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'risen' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'бежать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'run' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'ran' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'run' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'сказать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'say' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'said' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'said' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'видеть'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'see' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'saw' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'seen' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'искать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'seek' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'sought' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'sought' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'продавать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'sell' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'sold' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'sold' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'посылать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'send' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'sent' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'sent' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'устанавливать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'set' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'set' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'set' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'трясти'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'shake' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'shook' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'shaken' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'сиять'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'shine' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'shone' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'shone' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'стрелять'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'shoot' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'shot' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'shot' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'показывать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'show' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'showed' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'shown' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'сжиматься'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'shrink' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'shrank' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'shrunk' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'закрывать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'shut' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'shut' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'shut' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'петь'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'sing' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'sang' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'sung' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'сидеть'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'sit' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'sat' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'sat' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'спать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'sleep' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'slept' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'slept' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'скользить'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'slide' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'slid' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'slid' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'пахнуть'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'smell' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'smelt' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'smelt' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'говорить'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'speak' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'spoke' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'spoken' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'произносить или писать по буквам'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'spell' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'spelt ' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'spelt ' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'тратить'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'spend' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'spent' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'spent' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'разлить'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'spill' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'spilt' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'spilt' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'крутить'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'spin' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'spun' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'spun' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'разделять'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'split' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'split' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'split' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'портить'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'spoil' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'spoilt' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'spoilt' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'распространять'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'spread' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'spread' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'spread' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'стоять'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'stand' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'stood' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'stood' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'воровать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'steal' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'stole' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'stolen' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'жалить'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'sting' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'stung' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'stung' AND [eng].[Type] = 10), 
	NULL
)
,
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'вонять'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'stink' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'stank' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'stunk' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'ударять'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'strike' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'struck' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'struck' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'клясться'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'swear' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'swore' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'sworn' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'подметать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'sweep' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'swept' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'swept' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'опухать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'swell' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'swelled' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'swollen' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'плавать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'swim' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'swam' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'swum' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'брать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'take' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'took' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'taken' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'обучать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'teach' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'taught' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'taught' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'рвать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'tear' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'tore' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'torn' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'рассказывать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'tell' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'told' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'told' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'думать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'think' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'thought' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'thought' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'бросать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'throw' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'threw' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'thrown' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'понимать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'understand' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'understood' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'understood' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'будить'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'wake' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'woke' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'woken' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'носить'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'wear' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'wore' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'worn' AND [eng].[Type] = 10), 
	'Одежду'
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'побеждать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'win' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'won' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'won' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'обматывать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'wind' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'wound' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'wound' AND [eng].[Type] = 10), 
	NULL
),
(
	(SELECT TOP 1 [rus].[Id] FROM [RussianUnits] AS [rus] WHERE [rus].[Value] = 'писать'), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'write' AND [eng].[Type] = 8), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'wrote' AND [eng].[Type] = 9), 
	(SELECT TOP 1 [eng].[Id] FROM [EnglishUnits] AS [eng] WHERE [eng].[Value] = 'written' AND [eng].[Type] = 10), 
	NULL
)