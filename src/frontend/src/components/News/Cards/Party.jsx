import React from 'react';

import Card from '@material-ui/core/Card';
import CardActionArea from '@material-ui/core/CardActionArea';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';

class PartyCard extends React.Component {
    render() {
        return(
            <Card style={{margin: '2%'}}>
            <CardContent>
                <Typography gutterBottom variant="h5" component="h2">
                    {this.props.title}
                </Typography>
                <Typography variant="body2" color="textSecondary" component="p">
                {this.props.text}
                </Typography>
                </CardContent>
            <CardActions>
                <Button size="small" color="primary">
                Перейдти
                </Button>
                
            </CardActions>
        </Card>
        )
    }
}

export default PartyCard