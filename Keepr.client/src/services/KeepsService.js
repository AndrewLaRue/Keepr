
import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"






class KeepsService {


  async getKeepsByProfileId(creatorId) {
    const res = await api.get('api/profiles/' + creatorId + '/keeps')
    logger.log(res.data, 'getting here')
    AppState.profileKeeps = res.data
  }


  // SECTION set active
  async setActiveKeep(id) {
    const res = await api.get('api/keeps/' + id)
    AppState.activeKeep = res.data
  }

  async setActiveVaultKeep(vaultKeep) {
    await api.get('api/keeps/' + vaultKeep.id)
    logger.log('set active vaultkeep', vaultKeep)
    AppState.activeKeep = vaultKeep
    logger.log('active vaultkeep', AppState.activeKeep)

  }

  // SECTION Get all
  async getAllKeeps() {
    const res = await api.get('api/keeps')
    logger.log('getting all keeps', res.data)
    AppState.keeps = res.data
  }
  // SECTION Get vaultKeeps
  async getVaultKeeps(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    logger.log('getting all vaultKeeps', res.data)
    AppState.vaultKeeps = res.data
  }


  async createKeep(keep) {
    const res = await api.post('api/keeps', keep)
    logger.log('creating a keep', res.data)
    AppState.keeps.push(res.data)
  }


  async deleteKeep(keepId) {
    await api.delete(`api/keeps/${keepId}`)

    AppState.keeps = AppState.keeps.filter(k => k.id != keepId)

    AppState.profileKeeps = AppState.profileKeeps.filter(k => k.id != keepId)

  }
  async deleteVaultKeep(vaultKeepId) {
    await api.delete(`api/vaultkeeps/${vaultKeepId}`)

    AppState.vaultKeeps = AppState.vaultKeeps.filter(v => v.id != vaultKeepId)


  }


  async addToVault(vaultId, keepId) {
    await api.post('api/vaultkeeps', vaultId, keepId)
  }
}

export const keepsService = new KeepsService()