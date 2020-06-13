import React from 'react';
import Card from '@material-ui/core/Card';
import CardActionArea from '@material-ui/core/CardActionArea';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Button from '@material-ui/core/Button';
import Divider from '@material-ui/core/Divider';
import Typography from '@material-ui/core/Typography';

import './index.css'

class ConfirmationNewUserCard extends React.Component { 
    constructor(props) {
        super(props);
        this.state = {
            status: 'show'
        }
    }
    render() {
        const hide = (result) => {
            this.setState({status: 'hide'})
        }
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
                        <Button size="small" color="primary" onClick={() => hide(1)}>
                            Принять
                        </Button>
                        <Button size="small" color="secondary" onClick={() => hide(0)}>
                            Отклонить
                        </Button>
                    </CardActions>
                </Card>
            </div>
        )
    }
}
export default ConfirmationNewUserCard

