package zielware.TicTacToe.Game.Board;

import org.springframework.data.repository.CrudRepository;
import org.springframework.transaction.annotation.Transactional;

public interface BoardRepository extends CrudRepository<Board, String> { 
    @Transactional
    Board findByuId(Integer uId);
}
