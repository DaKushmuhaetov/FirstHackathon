// React
import React from 'react'

// Router
import {withRouter} from 'react-router-dom'

// Styles
import './index.css'

class Login extends React.PureComponent {
    render() {
        return (
            <div>
                Login Page
            </div>
        )
    }
}

export default withRouter(Login)