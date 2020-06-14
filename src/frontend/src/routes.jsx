import React from 'react'
import {Switch, Route, Redirect, withRouter} from 'react-router-dom'

import Main from './components/Main'
import Login from './components/Login'
import Registration from './components/Registration'

import UserPanel from './components/UserPanel'
import HousePanel from './components/HousePanel'
import HouseLogin from './components/HouseLogin'
import HouseRegistration from './components/HouseRegistration'

import Meetings from './components/Meetings'
import Votings from './components/Votings'
import News from './components/News'
import UserList from './components/UserList'
import InDevelopment from './components/InDevelopment'

// Context
import {Context} from './context'

class Routers extends React.PureComponent {
    static contextType = Context

    render() {
        const {isAuth} = this.props

        if (isAuth) {
            if (this.context.isAdmin()) {
                return (
                    <Switch>
                        <Route
                            exact
                            path={'/'}
                            render={() => <HousePanel></HousePanel>}
                        />

                        <Route
                            exact
                            path={'/meetings'}
                            render={() => <HousePanel><Meetings/></HousePanel>}
                        />

                        <Route
                            exact
                            path={'/news'}
                            render={() => <HousePanel><News/></HousePanel>}
                        />

                        <Route
                            exact
                            path={'/votings'}
                            render={() => <HousePanel><Votings/></HousePanel>}
                        />

                        <Route
                            exact
                            path={'/chat'}
                            render={() => <HousePanel><InDevelopment/></HousePanel>}
                        />

                        <Route
                            exact
                            path={'/tasks'}
                            render={() => <HousePanel><InDevelopment/></HousePanel>}
                        />

                        <Route
                            exact
                            path={'/payments'}
                            render={() => <HousePanel><InDevelopment/></HousePanel>}
                        />

                        <Route
                            exact
                            path={'/users'}
                            render={() => <HousePanel><UserList/></HousePanel>}
                        />
    
                        <Redirect to={'/'}/>
                    </Switch>
                )
            } else {
                return (
                    <Switch>
                        <Route exact
                            path={'/'}
                            render={() => <UserPanel></UserPanel>}
                        />

                        <Route
                            exact
                            path={'/meetings'}
                            render={() => <UserPanel><Meetings/></UserPanel>}
                        />

                        <Route
                            exact
                            path={'/news'}
                            render={() => <UserPanel><News/></UserPanel>}
                        />

                        <Route
                            exact
                            path={'/votings'}
                            render={() => <UserPanel><Votings/></UserPanel>}
                        />

                        <Route
                            exact
                            path={'/chat'}
                            render={() => <UserPanel><InDevelopment/></UserPanel>}
                        />

                        <Route
                            exact
                            path={'/tasks'}
                            render={() => <UserPanel><InDevelopment/></UserPanel>}
                        />

                        <Route
                            exact
                            path={'/payments'}
                            render={() => <UserPanel><InDevelopment/></UserPanel>}
                        />
    
                        <Redirect to={'/'}/>
                    </Switch>
                )
            }
        }

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

                <Route exact
                    path={'/house/login'}
                    render={() => <HouseLogin/>}
                />

                <Route exact
                    path={'/house/registration'}
                    render={() => <HouseRegistration/>}
                />
    
                <Redirect to={'/'}/>
            </Switch>
        )
    }
}

export default withRouter(Routers)