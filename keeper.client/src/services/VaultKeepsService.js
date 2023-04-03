import { AppState } from "../AppState"
import { api } from "./AxiosService"

class VaultKeepsService{

  async deleteVaultKeep(vaultKeepId){
    const res = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
    const index = AppState.keeps.findIndex( k => k.vaultKeepId == vaultKeepId)
    AppState.keeps.splice(index, 1)
  }

  async addKeepToVault(VaultKeepData){
    const res = await api.post('api/vaultkeeps', VaultKeepData)
  }

}

export const vaultKeepsService = new VaultKeepsService()