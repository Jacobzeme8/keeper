import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class VaultsService{

  async getMyVaults(){
    const res = await api.get('account/vaults')
    logger.log(res.data)
    AppState.myVaults = res.data
    AppState.vaults = res.data
  }

  async getVaultById(vaultId){
    const res = await api.get(`api/vaults/${vaultId}`)
    AppState.activeVault = res.data
  }



}

export const vaultsService = new VaultsService();