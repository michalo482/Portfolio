package zielware.TicTacToe.Game.Board;

import java.util.ArrayList;
import java.util.List;
import java.util.logging.Level;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class BoardService {

	@Autowired
	private BoardRepository boardRepository;
	
	public List<Board> getAllBoards() {
		List<Board> boards = new ArrayList<>();
		boardRepository.findAll().forEach(boards::add);	
		return boards;
	}
	
	public Board getBoard(Integer uId) {
		return boardRepository.findByuId(uId);
	}
	
	public void addBoard(Board board) {
		boardRepository.save(board);
	}
	
	public void updateBoard(Board board) {
		boardRepository.save(board);
	}
}
