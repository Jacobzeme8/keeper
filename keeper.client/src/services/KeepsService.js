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

}

export const keepsService = new KeepsService();