public interface ITransfer
{
    bool HasSpace();
    float Get(float amount);
    float Put(float amount);
}
