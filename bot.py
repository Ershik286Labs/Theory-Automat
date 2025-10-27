import sys

ROCK = 1  # Камень
PAPER = 2  # Бумага
SCISSORS = 3  # Ножницы

# Константы событий игры
WIN = 1
LOSE = 2
DRAW = 3

# Глобальная переменная для AI
ai = None

def set_parameters(set_count: int, wins_per_set: int) -> None:
    pass

def on_game_start() -> None:
    global ai
    ai = AI()

def choose(enemyElement: int) -> int:
    global ai
    ai.AddEvent(enemyElement)
    ai_move = ai.GenerateMove()
    ai.pastGameElements.append(ai_move)
    return ai_move

def on_game_end() -> None:
    pass

class AI:
    def __init__(self):
        self.choiseElement = SCISSORS
        self.pastGameEnemyElements = []
        self.pastGameElements = []
        self.pastGameEvents = []
        self.enemyStrategy = 0  # Единая стратегия противника
        
    def GenerateMove(self):
        if not self.pastGameEvents:
            self.choiseElement = SCISSORS
            return self.choiseElement
        
        lastEnemyElement = self.pastGameEnemyElements[-1]
        
        self.choiseElement = self.PredictCounterMove(lastEnemyElement, self.enemyStrategy)
        return self.choiseElement
    
    def PredictCounterMove(self, lastEnemyElement, strategy):
        if strategy == 0:
            predicted = lastEnemyElement
        elif strategy == 1:
            predicted = self.GetWinningElement(lastEnemyElement)
        else:
            predicted = SCISSORS
        
        return self.GetWinningElement(predicted)
    
    def AddEvent(self, enemyElement):
        if self.pastGameElements and self.pastGameEnemyElements:
            last_ai_element = self.pastGameElements[-1]
            last_enemy_element = self.pastGameEnemyElements[-1]
            gameEvent = self.CheckGame(last_enemy_element, last_ai_element)
            self.pastGameEvents.append(gameEvent)
        
        self.pastGameEnemyElements.append(enemyElement)
        
        if len(self.pastGameEnemyElements) >= 2:
            self.AnalyzeRecentStrategy()
    
    def AnalyzeRecentStrategy(self):
        strategies = [0, 0, 0]  # 0-повтор, 1-контра, 2-другое
        
        start_index = max(0, len(self.pastGameEnemyElements) - 5)
        
        for i in range(start_index + 1, len(self.pastGameEnemyElements)):
            previous = self.pastGameEnemyElements[i-1]
            current = self.pastGameEnemyElements[i]
            
            if previous == current:
                strategies[0] += 1
            elif current == self.GetWinningElement(previous):
                strategies[1] += 1
            else:
                strategies[2] += 1
        
        self.enemyStrategy = strategies.index(max(strategies))
        
    
    def CheckGame(self, playerElement, aiElement):
        if playerElement == aiElement:
            return DRAW
        
        if playerElement == ROCK:
            return WIN if aiElement == SCISSORS else LOSE
        elif playerElement == SCISSORS:
            return WIN if aiElement == PAPER else LOSE
        elif playerElement == PAPER:
            return WIN if aiElement == ROCK else LOSE
        
        return DRAW
    
    def GetWinningElement(self, element):
        if element == ROCK:
            return PAPER
        elif element == PAPER:
            return SCISSORS
        elif element == SCISSORS:
            return ROCK
        else:
            return ROCK