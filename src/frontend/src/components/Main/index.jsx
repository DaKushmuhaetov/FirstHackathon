// React
import React from 'react'

// Router
import {withRouter} from 'react-router-dom'

// Styles
import './index.css'

class Main extends React.PureComponent {
    render() {
        return (
            <div className="main">
                <div className="main-inner">

                    <div className="main-center">
                        Main page
                    </div>

                </div>
            </div>
        )
    }
}

export default withRouter(Main)