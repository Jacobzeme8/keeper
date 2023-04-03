import { api } from "./AxiosService";

class VaultsService{

  async getMyVaults(){
    await api.get('/account/vaults')
  }

}

export const vaultsService = new VaultsService();