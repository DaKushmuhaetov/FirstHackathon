import React from 'react';
import Card from '@material-ui/core/Card';
import CardHeader from '@material-ui/core/CardHeader';
import CardMedia from '@material-ui/core/CardMedia';
import CardContent from '@material-ui/core/CardContent';
import CardActions from '@material-ui/core/CardActions';
import IconButton from '@material-ui/core/IconButton';
import Typography from '@material-ui/core/Typography';
import { red } from '@material-ui/core/colors';
import FavoriteIcon from '@material-ui/icons/Favorite';
import ShareIcon from '@material-ui/icons/Share';
import { withStyles } from '@material-ui/core/styles'
import Vote from './Vote'


const styles = theme => ({
  root: {
    width: '96%',
    margin: 'auto',
  },
  choseRoot: {
    width: 'auto',
  },

  media: {
    height: 0,
    paddingTop: '56.25%', // 16:9
  },
  expand: {
    transform: 'rotate(0deg)',
    marginLeft: 'auto',
    transition: theme.transitions.create('transform', {
      duration: theme.transitions.duration.shortest,
    }),
  },
  expandOpen: {
    transform: 'rotate(180deg)',
  },
  avatar: {
    backgroundColor: red[500],
  },
  wait: {
    color: red,
  }
});



class VotingCard extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      vote: {
        stady: "wait",
        value: -1,
        variants: [
          {value: 0, name: "Вариант 1", perc: '12%'},
          {value: 1, name: "Вариант 2", perc: '26%'},
          {value: 2, name: "Вариант 3", perc: '10%'},
          {value: 3, name: "Вариант 4", perc: '52%'}
        ]
      }
    };
  }
  render() {
    const {classes} = this.props
    
    return (
      <Card className={classes.root}>
          <CardHeader
              title={this.props.title}
              subheader={this.props.date}
          />
          <CardMedia
              className={classes.media}
              image={this.props.image}
              title="Paella dish"
          />
          <CardContent>
              <Typography variant="body2" color="textSecondary" component="p">
              {this.props.textMini}
              </Typography>
              <Vote vote={this.state.vote} quest={this.props.quest}/>
          </CardContent>
          <CardActions disableSpacing>
              <IconButton aria-label="add to favorites">
              <FavoriteIcon />
              </IconButton>
              <IconButton aria-label="share">
              <ShareIcon />
              </IconButton>
          </CardActions>
      </Card>
    );
  }
}
export default withStyles(styles)(VotingCard)