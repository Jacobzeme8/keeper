namespace keeper.Services
{
  public class KeepsService
    {
        private readonly KeepsRepository _repo;

    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }

    internal Keep CreateKeep(Keep keepData, Account userInfo)
    {
        keepData.CreatorId = userInfo.Id;
        keepData.Creator = userInfo;
        Keep keep = _repo.CreateKeep(keepData);
        return keep;
    }

    internal string DeleteKeep(int id, Account userInfo)
    {
        Keep keep = this.GetOneKeep(id);
        if(keep.CreatorId != userInfo.Id) throw new Exception("NOt your keep to edit man");
        int rows = _repo.DeleteKeep(id);
        if(rows != 1) throw new Exception($"something when wrong man {rows} were deleted");
        return $"{rows} keeps were deleted";
    }

    internal Keep EditKeep(int id, Keep updata, Account userInfo)
    {
        Keep original = this.GetOneKeep(id);
        if(original.CreatorId != userInfo.Id) throw new Exception("not your keep to edit man find your own");
        original.Description = updata.Description != null ? updata.Description : original.Description;
        original.Name = updata.Name != null ? updata.Name : original.Name;
        original.Img = updata.Img != null ? updata.Img : original.Img;
        int rows = _repo.EditKeep(original);
        if (rows != 1) throw new Exception($"something went really wrong man check the db, eddited {rows} rows");
        return original;
    }

    internal List<Keep> GetAllKeeps()
    {
        List<Keep> keeps = _repo.GetAllKeeps();
        return keeps;
    }

    internal Keep GetOneKeep(int id)
    {
        Keep keep = _repo.GetOneKeep(id);
        if(keep == null) throw new Exception("bad keep id man");
        return keep;

    }
    }
}