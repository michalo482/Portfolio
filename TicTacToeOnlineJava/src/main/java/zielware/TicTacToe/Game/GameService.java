package zielware.TicTacToe.Game;

import java.util.ArrayList;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class GameService {

	@Autowired
	private GameRepository gameRepository;
	
	public List<Game> getAllGames() {
		List<Game> games = new ArrayList<>();
		gameRepository.findAll().forEach(games::add);	
		return games;
	}

	public Game getGame(Integer uId) {
		return gameRepository.findByuId(uId);
	}

	public void addGame(Game game) {
		gameRepository.save(game);
	}

	public void updateGame(Game game) {
		gameRepository.save(game);
	}

	public void delGame(String id) {
		gameRepository.deleteById(id);
	}
}