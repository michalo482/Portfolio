package zielware.TicTacToe.Joi;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpHeaders;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import zielware.TicTacToe.Game.Game;
import zielware.TicTacToe.Game.GameService;
import zielware.TicTacToe.utils.Const;
import zielware.TicTacToe.utils.TokenUtils;

@RestController
public class JoiController {

	private JoiService joiService;
	private TokenUtils tokenService;
	private GameService gameService;
	
	@RequestMapping("/joins")
	public List<Joi> getAllJois() {
		return joiService.getAllJois();
	}
	
	@RequestMapping(method=RequestMethod.POST, value="/game/{id}/join")
	public ResponseEntity<String> joinGame(@PathVariable Integer id) {	
		
		//find and return game by id
		Game game = gameService.getGame(id);
		if(null != game && (!game.isThereSecondPlayer() )) {			

			Joi secondJoiner = createSecondPlayer(game);
			joiService.addJoi(secondJoiner);
			
			game.setThereSecondPlayer(true);
			game.setFirstPlayerTurn(true);
			gameService.addGame(game);
			
		    HttpHeaders responseHeaders = new HttpHeaders();
		    responseHeaders.set(Const.SET_AUTH_TOKEN, secondJoiner.getToken().toString());
		 
		    String body = "{ \"invitationUrl\": \"127.0.0.1:8087/game/{" + game.getuId().toString() + "}/join\"}";
		    return ResponseEntity.ok()
		      .headers(responseHeaders)
		      .body(body);
		} else {
			return ResponseEntity.badRequest().body("Cannot join to that game!");
		}
	}

	private Joi createSecondPlayer(Game game) {
		Joi j = new Joi();
		j.setGame(game);
		j.setToken(tokenService.generateToken());
		j.setFirstPlayer(false);
		return j;
	}
}