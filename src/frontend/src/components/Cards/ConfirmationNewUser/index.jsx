import React from 'react';
import Card from '@material-ui/core/Card';
import CardActionArea from '@material-ui/core/CardActionArea';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Button from '@material-ui/core/Button';
import Divider from '@material-ui/core/Divider';
import Typography from '@material-ui/core/Typography';

import Http from '../../../modules/http'
import {Context} from '../../../context'

import './index.css'

class ConfirmationNewUserCard extends React.Component {
    static contextType = Context

    state = {
        status: 'show'
    }

    handleSet = async (action) => {
        let http = new Http(`/house/person/${this.props.id}/${action ? 'accept' : 'reject'}`, "POST", null, {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${this.context.token}`
        })

        const response = await http.request().catch((status) => {
            console.log(status)
            return undefined
        })

        if (response === undefined) return

        this.props.hide(this.props.index)
    }

    render() {
        return (
            <div className={this.state.status + " root"}>
                <Card className="card">
                    <CardContent>
                        <Typography gutterBottom variant="h5" component="h2">
                            {this.props.username}
                        </Typography>
                        <Divider />
                        <Typography style={{marginBottom: '8px', marginTop: '8px'}} variant="body2" color="textSecondary" component="p">
                            {this.props.address}
                        </Typography>
                        <Typography variant="body2" color="textSecondary" component="p">
                            {this.props.email}
                        </Typography>
                    </CardContent>
                    <CardActions style={{marginTop: '-16px'}}>
                        <Button size="small" color="primary" onClick={() => this.handleSet(true)}>
                            Принять
                        </Button>
                        <Button size="small" color="secondary" onClick={() => this.handleSet(false)}>
                            Отклонить
                        </Button>
                    </CardActions>
                </Card>
            </div>
        )
    }
}
export default ConfirmationNewUserCard

