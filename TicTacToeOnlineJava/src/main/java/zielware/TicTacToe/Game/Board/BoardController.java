package zielware.TicTacToe.Game.Board;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

public class BoardController {

	@Autowired
	private BoardService boardService;
	
	@RequestMapping(method=RequestMethod.POST, value="/board")
	public void createBoard() {
		
		Board board = new Board();
		boardService.addBoard(board);
	}
	

}
