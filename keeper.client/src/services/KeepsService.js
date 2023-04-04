import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class KeepsService{

  async getAllKeeps(){
    const res = await api.get('api/keeps')
    logger.log(res.data)
    AppState.keeps = res.data
  }

  async getActiveKeep(keepId){
    const res = await api.get(`api/keeps/${keepId}`)
    AppState.activeKeep = res.data
  }

  async getMyKeeps(){
    const res = await api.get(`api/profiles/${AppState.account.id}/keeps`)
    AppState.keeps = res.data
  }

  async getKeepsInVault(vaultId){
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    logger.log(res.data)
    AppState.keeps = res.data
  }

  async getProfileKeeps(id){
    const res = await api.get(`api/profiles/${id}/keeps`)
    AppState.keeps = res.data
  }

  async createKeep(keepData, route){
    const res = await api.post('api/keeps', keepData)
    logger.log(res.data)
    if(!route.path.includes('profile')){
      AppState.keeps.push(res.data)
    }
  }

  async deleteKeep(keepId){
    const res = await api.delete(`api/keeps/${keepId}`)
    const index = AppState.keeps.findIndex(k => k.id == keepId)
    AppState.keeps.splice(index, 1)
  }

}

export const keepsService = new KeepsService();