import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"






class VaultsService {


  async getVaultsByProfileId(creatorId) {
    const res = await api.get('api/profiles/' + creatorId + '/vaults')
    logger.log(res.data, 'getting here')
    AppState.profileVaults = res.data
  }


  // SECTION set active
  async setActiveVault(id) {
    const res = await api.get('api/vaults/' + id)
    AppState.activeVault = res.data

  }

  // SECTION Get all
  async getAllVaults() {
    const res = await api.get('api/vaults')
    logger.log('getting all vaults', res.data)
    AppState.vaults = res.data
  }

  async createVault(vault) {
    const res = await api.post('api/vaults', vault)
    logger.log('creating a vault', res.data)
    AppState.myVaults.push(res.data)
  }



  async deleteVault(vaultId) {
    await api.delete(`api/vaults/${vaultId}`)
    AppState.vaults = AppState.vaults.filter(v => v.id != vaultId)
    AppState.profileVaults = AppState.profileVaults.filter(v => v.id != vaultId)
  }
}

export const vaultsService = new VaultsService()