import React from 'react';
import Radio from '@material-ui/core/Radio';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import RadioGroup from '@material-ui/core/RadioGroup';
import Typography from '@material-ui/core/Typography';
import FormControl from '@material-ui/core/FormControl';
import FormLabel from '@material-ui/core/FormLabel';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import CardActions from '@material-ui/core/CardActions';
import Button from '@material-ui/core/Button';
import {Context} from '../../../context'
import Http from '../../../modules/http'

import './index.css'


class Vote extends React.Component {    
  static contextType = Context

  

  constructor(props) {
    super(props);
    this.state = {
      stady: this.props.vote.stady,
      value: this.props.vote.value,
      percs: [0, 0, 0]
    }
    this.progressBars = [];
  }
  setVote = async (id) => {
    console.log(id)
    const data = JSON.stringify({
      votingId: this.props.votingId,
      variantId: id
    })
        
    let http = new Http(`/votings/${this.props.votingId}/vote?variantId=${id}`, 'POST', data, {'Authorization': `Bearer ${this.context.token}`})
    const response = await http.request().catch((status) => {
        console.log(status);
        return undefined
    })
    
  }
  render() {
    console.log(this.props.votingId)

    let votes = this.props.vote.variants;


    const handleExpandClick = () => {
      if(this.state.value === - 1) {
        
        this.context.handleToast("Выберите хотя бы 1 вариант", "#F00", 1500)
        return
      }
      this.setState({stady: "voted"})
      this.setState({progressBars: []})
      for(let i = 0; i < this.progressBars.length; i++) {
        this.progressBars[i].setAttribute("style", `width: ${votes[i].count}%`)
      }
      this.setVote(this.props.vote.variants[this.state.value].id)

    };

    const handleChange = (event) => {
      if(this.state.stady === "voted")
        return;
      this.setState({value: event.target.value});
    };
    
    return (
      <div className="nested">
        <Card>
          <CardContent>
            <Typography variant="h5" component="h2">
              {this.props.quest}
            </Typography>
            <FormControl className="vote-root" component="fieldset">
              <FormLabel component="legend" className="title-prompt">Выберите:</FormLabel>
                <RadioGroup className={this.state.stady+" chose-root"} aria-label="voting" name="voting1" value={this.state.value} onChange={handleChange}>
                  {votes.map((value, index) => {
                    return (
                    <div className="variant-block" key={index}>
                      <FormControlLabel className="point" value={`${index}`} control={<Radio color="primary"/>} label={value.title} />
                      <div className="percentage">{value.count}</div>
                      <div ref={(input) => {this.progressBars[index] = input}} className="progress-bar">  </div>
                    </div>
                    )
                  })}
                </RadioGroup>
            </FormControl>                
          </CardContent>
          <CardActions>
            <Button variant="contained" onClick={handleExpandClick}color="primary">Проголосовать</Button>
          </CardActions>
        </Card>
      </div>
      
    )
    
    }
  }

export default Vote