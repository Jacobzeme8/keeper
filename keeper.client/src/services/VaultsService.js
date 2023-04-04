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

  async getProfileVaults(id){
    const res = await api.get(`api/profiles/${id}/vaults`)
    AppState.vaults = res.data
  }


  async getOnlyMyVaults(){
    const res = await api.get('account/vaults')
    AppState.myVaults = res.data
  }


  async createVault(vaultData){
    if(vaultData.isPrivate == null){vaultData.isPrivate = false}
    const res = await api.post('api/vaults', vaultData)
    AppState.myVaults.push(res.data)
  }

  async deleteVault(vaultId){
    const res = await api.delete(`api/vaults/${vaultId}`)
    logger.log(res.data)
    const index = AppState.vaults.findIndex(v => v.id == vaultId)
    logger.log(index, "Index")
    AppState.vaults.splice(index, 1)
  }



}

export const vaultsService = new VaultsService();