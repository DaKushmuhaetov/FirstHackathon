// React
import React from 'react'

import InfoCard from '../Cards/Info'
import Image from '../../images/gopniki.jpg'
// MaterialUI
import Grid from '@material-ui/core/Grid'
import Pagination from '@material-ui/lab/Pagination';

import './index.css'
import {Context} from '../../context'


import Http from '../../modules/http'

class News extends React.PureComponent {
    static contextType = Context

    constructor(props) {
        super(props) 
        
        this.state =  {
            total: 1,
            items: [
              ]
        }
    }

    parseImage = (data) => {
        if(data != undefined)
            return `data:image/jpeg;base64,${data}`
        else
            return Image
        

    }

    componentDidMount() {
        this.onChange(undefined, 1)
    }

    onChange = async (e, page) => {
        let http = new Http(`/house/news`, 'GET', null, {'Authorization': `Bearer ${this.context.token}`})

        const response = await http.request().catch((status) => {
            console.log(status);
            return undefined
        })

        console.log(response.items)
        this.setState({items: response.items})
        this.setState({total: response.total/3})
    }

    render() {
        return (
            <div>
                <Grid container spacing={3}>   
                    {this.state.items.map((value, index) => {
                        return(
                            <Grid key={index} item xs={12} sm={4}>
                                <InfoCard 
                                        title={value.title}
                                        date={value.createDate.substring(0, 10) + ' ' + value.createDate.substring(11)}
                                        image={this.parseImage(value.image)}
                                        text={value.description}
                                    />
                            </Grid>    
                        )
                    })}
                </Grid>
                <Pagination className='pogination' count={this.state.total} color="primary" onChange={this.onChange}/>
            </div>
            
        )
    }
}

export default News