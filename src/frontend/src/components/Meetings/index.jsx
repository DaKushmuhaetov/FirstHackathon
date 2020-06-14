// React
import React from 'react'

// MaterialUI
import Grid from '@material-ui/core/Grid'

import VotingCard from '../Cards/Party'
import Http from '../../modules/http'
import {Context} from '../../context'


class Meetings extends React.PureComponent {
    static contextType = Context

    constructor(props) {
        super(props) 
 
        this.state =  {
            items: [
              ]
        }
    }
    componentDidMount() {
        this.onChange(undefined, 1)
    }

    onChange = async (e, page) => {
        let http = new Http(`/meetings`, 'GET', undefined, {'Authorization': `Bearer ${this.context.token}`})
        const response = await http.request().catch((status) => {
            console.log(status);
            return undefined
        })
        console.log(response.items)
        this.setState({items: response.items})


    }
    render() {
        return (
            <Grid container spacing={3}>            
                {this.state.items.map((value, index) => {
                        return(
                            <Grid key={index} item xs={12} sm={4}>
                                <VotingCard 
                                        title={value.title}
                                        date={value.meetingDate.substring(0, 10) + ' ' + value.meetingDate.substring(11)}
                                        text={value.description}
                                    />
                            </Grid>    
                        )
                    })}
            </Grid>
        )
    }
}

export default Meetings