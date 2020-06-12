// React
import React from 'react'

// Router
import {withRouter} from 'react-router-dom'
import Routers from '../../routes'

// Context
import {Context} from '../../context'

// Toast
import Toast from '../../modules/toast/index.jsx'
import '../../modules/toast/index.css'

// Styles
import './index.css'

class App extends React.PureComponent {
    toastBox = React.createRef()
    main = React.createRef()

    componentDidMount() {
        Toast.Initialize(this.toastBox.current, this.main.current) // Привязываем тосты
    }

    handleToast = (text, color, ms) => { // Пример: this.context.handleToast('Вызов тостов в handleToast', '#8BC34A', 5000)
        new Toast(text, color, ms)
    }

    render() {
        return (
            <Context.Provider value={{
                handleToast: this.handleToast
            }}>
                <div ref={this.toastBox} id="toast-box"/>
                <main ref={this.main}>
                    <Routers/>
                </main>
            </Context.Provider>
        )
    }
}

export default withRouter(App)