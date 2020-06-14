// React
import React from 'react'

// MaterialUI
import Grid from '@material-ui/core/Grid'

import Image from '../../images/gopniki.jpg'

import Http from '../../modules/http'
import VotingCard from '../Cards/Voting/'

import {Context} from '../../context'

class Votings extends React.PureComponent {
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
        let http
        if(this.context.isAdmin()){
            http = new Http(`/house/votings`, 'GET', undefined, {'Authorization': `Bearer ${this.context.token}`})
        } else {
            http = new Http(`/votings`, 'GET', undefined, {'Authorization': `Bearer ${this.context.token}`})
        }
        const response = await http.request().catch((status) => {
            console.log(status);
            return undefined
        })
        
        // Рамиль ******, говорил в процентах отправляй, ****, нет, *****, число вернул
 
        this.setState({items: response.items})
    }
    render() {
        console.log('')
        return (
            <Grid container spacing={3}>            
                {this.state.items.map((value, index) => {
                        return(
                            <Grid key={index} item xs={12} sm={4}>
                                <VotingCard 
                                        votingID={value.id}
                                        title={value.title}
                                        date="14.06.2020"
                                        image={Image}
                                        variants={value.variants}
                                    />
                            </Grid>    
                        )
                    })}
            </Grid>
        )
    }
}

export default Votings