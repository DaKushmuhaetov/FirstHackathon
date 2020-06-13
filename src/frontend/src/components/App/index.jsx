// React
import React from 'react'

// Router
import {withRouter} from 'react-router-dom'
import Routers from '../../routes'

// Redux
import { connect } from 'react-redux'
import {updateData} from '../../redux/app'

// Context
import {Context} from '../../context'

// Toast
import Toast from '../../modules/toast/index.jsx'
import '../../modules/toast/index.css'

// Modules
import Auth from '../../modules/auth'
import Http from '../../modules/http'

// Styles
import './index.css'

class App extends React.PureComponent {
    // Для тостов
    toastBox = React.createRef()
    main = React.createRef()

    // Для авторизации
    auth = new Auth()
    isAuthed = !!this.auth.token

    isAdmin = () => {
        const token = this.parseJwt(this.auth.token)

        return token.iss === 'FirstHackathon.Backend.Admin'
    }

    async componentDidMount() {
        Toast.Initialize(this.toastBox.current, this.main.current) // Привязываем тосты

        if (this.isAuthed) {
            const data = this.parseJwt(this.auth.token)

            let http

            if (this.isAdmin()) {
                http = new Http("/house/token", "POST", null, {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${this.auth.token}`
                })
            } else {
                http = new Http("/person/token", "POST", null, {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${this.auth.token}`
                })
            }
    
            const response = await http.request().catch((status) => {
                if (status === 401) this.auth.logout()

                return undefined
            })

            if (response === undefined) return

            const prefix = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/'

            if (this.isAdmin()) {
                const login = data[`${prefix}emailaddress`]
                const id = data[`${prefix}nameidentifier`]
                const address = data[`${prefix}streetaddress`]

                this.props.updateData({ login, id, address })
            } else {
                const email = data[`${prefix}emailaddress`]
                const name = data[`${prefix}name`]
                const surname = data[`${prefix}surname`]
                const id = data[`${prefix}nameidentifier`]
                const address = data[`${prefix}streetaddress`]

                this.props.updateData({ email, name, surname, id, address })
            }
        }
    }

    handleToast = (text, color, ms) => { // Пример: this.context.handleToast('Вызов тостов в handleToast', '#8BC34A', 5000)
        new Toast(text, color, ms)
    }

    parseJwt = (token) => {
        let base64Url = token.split('.')[1]
        let base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/')
        let jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)
        }).join(''))
    
        return JSON.parse(jsonPayload)
    }

    render() {
        return (
            <Context.Provider value={{
                handleToast: this.handleToast,
                token: this.auth.token,
                login: this.auth.login,
                logout: this.auth.logout,
                isAuthed: this.isAuthed,
                parseJwt: this.parseJwt,
                isAdmin: this.isAdmin
            }}>
                <div ref={this.toastBox} id="toast-box"/>
                <main className="bg" ref={this.main}>
                    <div className="bg-inner">
                        <Routers isAuth={this.isAuthed}/>
                    </div>
                </main>
            </Context.Provider>
        )
    }
}

const mapDispatchToProps = {
    updateData
}

export default withRouter(connect(null, mapDispatchToProps)(App))