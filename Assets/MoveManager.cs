public interface MoveManager{
    void OnMove(Branch branch1, Branch branch2);

    bool IsMoveValid(Branch branch1, Branch branch2);    
}
