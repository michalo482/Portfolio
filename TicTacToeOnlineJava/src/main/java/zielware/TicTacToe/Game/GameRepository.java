package zielware.TicTacToe.Game;

import org.springframework.data.repository.CrudRepository;
import org.springframework.transaction.annotation.Transactional;


public interface GameRepository extends CrudRepository<Game, String> { 
    @Transactional
	Game findByuId(Integer uId);
}
