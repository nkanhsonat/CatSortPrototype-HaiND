public interface IMoveManager{
    void OnMove(Branch branch1, Branch branch2);

    bool IsMoveValid(Branch branch1, Branch branch2);    
}
