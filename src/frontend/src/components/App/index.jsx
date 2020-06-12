// React
import React from 'react'

// Router
import {withRouter} from 'react-router-dom'
import Routers from '../../routes'

// Styles
import './index.css'

class App extends React.PureComponent {
    render() {
        return (
            <main>
                <Routers/>
            </main>
        )
    }
}

export default withRouter(App)