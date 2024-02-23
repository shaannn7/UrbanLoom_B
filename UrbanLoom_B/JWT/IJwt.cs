namespace UrbanLoom_B.JWT
{
    public interface IJwt
    {
        int GetUserIdFromToken(string token);
    }
}
