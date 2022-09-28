import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async editAccount(accountData) {
    const res = await api.put('account', accountData)
    AppState.account = new Account(res.data)
  }

  async getMyVaults() {
    const res = await api.get('/account/vaults')
    logger.log('getting my vaults', res.data)
    AppState.myVaults = res.data
  }

  async getVaultsByAccountId(accountId) {
    try {
      const res = await api.get('/account/vaults', accountId)
      logger.log('getting my vaults', res.data)
      AppState.accountVaults = res.data
    }
    catch (error) {
      logger.error("[Getting Profile Vaults]", error);
      Pop.error(error);
    }
  }

}

export const accountService = new AccountService()
