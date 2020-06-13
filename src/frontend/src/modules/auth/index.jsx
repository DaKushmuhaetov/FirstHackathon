const storageName = 'token'

export default class Auth {
    constructor(token) {
        this.token = token

        let data = localStorage.getItem(storageName)

        if (data) {
            this.login(data)
        }
    }

    login(jwtToken) {
        this.token = jwtToken

        localStorage.setItem(storageName, jwtToken)
    }

    logout() {
        localStorage.removeItem(storageName)
    }
}