namespace keeper.Repositories
{
  public class KeepsRepository
    {
        private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        string sql = @"
        INSERT INTO keeps
        (creatorId, name, description, img)
        VALUES
        (@creatorId, @name, @description, @img);
        SELECT LAST_INSERT_ID()
        ;";
        int id = _db.ExecuteScalar<int>(sql, keepData);
        keepData.Id = id;
        return keepData;

    }

    internal int DeleteKeep(int id)
    {
        string sql = @"
        DELETE FROM keeps
        WHERE id = @id;
        ";

        int rows = _db.Execute(sql, new{id});
        return rows;
    }

    internal int EditKeep(Keep original)
    {
        string sql = @"
        UPDATE keeps
        SET
        description = @description,
        name = @name,
        img = @img
        WHERE id = @id;
        ";

        int rows = _db.Execute(sql, original);
        return rows;
    }

    internal List<Keep> GetAllKeeps()
    {
        string sql = @"
        SELECT
        keeps.*,
        creator.*
        FROM keeps
        JOIN accounts creator ON keeps.creatorId = creator.id
        ;";

        List<Keep> keeps = _db.Query<Keep, Account, Keep>(sql, (keep, creator)=>{
            keep.Creator = creator;
            return keep;
        }).ToList();
        return keeps;
    }

    internal Keep GetOneKeep(int id)
    {
        string sql = @"
        UPDATE keeps
        SET
        views = views + 1
        WHERE id = @id;

        SELECT
        keeps.*,
        creator.*
        FROM keeps
        JOIN accounts creator ON keeps.creatorId = creatorId
        WHERE keeps.id = @id;        
        ";

        Keep keep = _db.Query<Keep, Account, Keep>(sql, (keep, creator) =>{
            keep.Creator = creator;
            return keep;
        }, new{id}).FirstOrDefault();
        return keep;
    }
    } 
}