import React from 'react'
import {Switch, Route, Redirect, withRouter} from 'react-router-dom'

import Main from './components/Main'
import Login from './components/Login'
import Registration from './components/Registration'

class Routers extends React.PureComponent {
    render() {
        return (
            <Switch>
                <Route exact
                    path={'/'}
                    render={() => <Main/>}
                />
    
                <Route exact
                    path={'/login'}
                    render={() => <Login/>}
                />
    
                <Route exact
                    path={'/registration'}
                    render={() => <Registration/>}
                />
    
                <Redirect to={'/'}/>
            </Switch>
        )
    }
}

export default withRouter(Routers)