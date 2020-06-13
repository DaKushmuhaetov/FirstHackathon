import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import clsx from 'clsx';
import Card from '@material-ui/core/Card';
import CardHeader from '@material-ui/core/CardHeader';
import CardMedia from '@material-ui/core/CardMedia';
import CardContent from '@material-ui/core/CardContent';
import CardActions from '@material-ui/core/CardActions';
import Collapse from '@material-ui/core/Collapse';
import IconButton from '@material-ui/core/IconButton';
import Typography from '@material-ui/core/Typography';
import { red } from '@material-ui/core/colors';
import FavoriteIcon from '@material-ui/icons/Favorite';
import ShareIcon from '@material-ui/icons/Share';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import MoreVertIcon from '@material-ui/icons/MoreVert';
import Button from '@material-ui/core/Button';
import Radio from '@material-ui/core/Radio';
import RadioGroup from '@material-ui/core/RadioGroup';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import FormControl from '@material-ui/core/FormControl';
import FormLabel from '@material-ui/core/FormLabel';

class Vote extends React.Component {
    render() {
      return <div>
          <FormControlLabel value={this.props.id} control={<Radio color="primary"/>} label={this.props.name} />
          
      </div>;
  }
}


const useStyles = makeStyles((theme) => ({
  root: {
    width: '96%',
    margin: 'auto',
  },
  choseRoot: {
    width: 'auto',
  },
  nested: {
    boxShadow: '0 0 5px rgba(0,0,0,0.5)',
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
}));

export default function VotingCard(props) {
  const classes = useStyles();
  const [expanded, setExpanded] = React.useState(false);

  const handleExpandClick = () => {
    setExpanded(!expanded);
    console.log("click")
  };
  const [value, setValue] = React.useState('female');
  const handleChange = (event) => {
    setValue(event.target.value);
  };
  let nvalue = 1;
  return (
    <Card className={classes.root}>
        <CardHeader
            title={props.title}
            subheader={props.date}
        />
        <CardMedia
            className={classes.media}
            image={props.image}
            title="Paella dish"
        />
        <CardContent>
            <Typography variant="body2" color="textSecondary" component="p">
            {props.textMini}
            </Typography>
            <Card className={classes.nested}>
              <CardContent>
                <Typography variant="h5" component="h2">
                  Title
                </Typography>
                <FormControl component="fieldset">
                <FormLabel component="legend">Выберите:</FormLabel>
                <RadioGroup className={classes.choseRoot} aria-label="voting" name="voting1" value={value} onChange={handleChange}>
                  <Vote id="0" name="Вариант 0"/>
                  <Vote id="1" name="Вариант 1"/>
                  <Vote id="2" name="Вариант 2"/>
                </RadioGroup>
              </FormControl>
                              
              </CardContent>
              <CardActions>
                <Button variant="contained" color="primary">Проголосовать</Button>
              </CardActions>
            </Card>
        </CardContent>
        <CardActions disableSpacing>
            <IconButton aria-label="add to favorites">
            <FavoriteIcon />
            </IconButton>
            <IconButton aria-label="share">
            <ShareIcon />
            </IconButton>
            <IconButton
            className={clsx(classes.expand, {
                [classes.expandOpen]: expanded,
            })}
            onClick={handleExpandClick}
            aria-expanded={expanded}
            aria-label="show more"
            >
            <ExpandMoreIcon />
            </IconButton>
        </CardActions>
        <Collapse in={expanded} timeout="auto" unmountOnExit>
            <CardContent>
            <Typography paragraph>Подробнее:</Typography>
            <Typography paragraph>
                {props.text}
            </Typography>
            </CardContent>
        </Collapse>
    </Card>
  );
}