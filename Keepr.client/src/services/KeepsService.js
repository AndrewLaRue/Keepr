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

  // SECTION Get all
  async getAllKeeps() {
    const res = await api.get('api/keeps')
    logger.log('getting all keeps', res.data)
    AppState.keeps = res.data
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
}

export const keepsService = new KeepsService()