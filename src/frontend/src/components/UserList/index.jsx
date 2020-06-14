// React
import React, {Fragment} from 'react'

// MaterialUI
import Grid from '@material-ui/core/Grid'

import Http from '../../modules/http'
import ConfirmationNewUserCard from '../Cards/ConfirmationNewUser'
import {Context} from '../../context'
import Typography from '@material-ui/core/Typography';

class UserList extends React.PureComponent {
    static contextType = Context

    state = {
        requests: []
    }

    async componentDidMount() {
        let http = new Http("/house/requests", "GET", null, {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${this.context.token}`
        })

        const response = await http.request().catch((status) => {
            console.log(status)
            return undefined
        })

        if (response === undefined) return

        this.setState({ requests: response.items })
    }

    hide = (index) => {
        let copy = this.state.requests.slice()

        copy.splice(index, 1)

        this.setState({ requests: copy })
    }

    render() {
        return (
            <Fragment>
                {this.state.requests.length > 0 ? (
                <Grid container spacing={3}>
                    {this.state.requests.map((value, index) => {
                            return(
                                <Grid key={index} item xs={12} sm={4}>
                                    <ConfirmationNewUserCard
                                        username={value.name + ' ' + value.surname}
                                        address={value.address}
                                        email={value.login}

                                        id={value.id}
                                        index={index}
                                        hide={this.hide}
                                    />
                                </Grid>    
                            )
                    })}
                </Grid>
                ) : null}

                {this.state.requests.length <= 0 ? (
                    <Typography className='tab-h4' variant="h4" component="h2">
                        Список заявлений на регистрацию пуст.
                    </Typography>
                ) : null}
            </Fragment>
        )
    }
}

export default UserList