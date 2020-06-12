export default class Http {
    constructor(url, method, body, headers) {
        this.url = `http://localhost:53167${url}` // your server url
        this.method = method || 'GET'
        this.body = body || null
        this.headers = headers || { 'Content-Type': 'application/json' }

        this.loading = false
        this.error = null
    }

    request = async() => {
        if (this.loading) return

        this.loading = true
        try {

            const response = await fetch(this.url, {
                method: this.method,
                body: this.body,
                headers: this.headers
            })

            let data

            try {
                data = await response.json()
            } catch {
                data = await response
            }

            this.loading = false

            if (!response.ok) {
                throw(response.status) // Это важно
            }

            return data

        } catch (err) {
            this.loading = false

            this.error = err.message

            throw(err)
        }
    }
}