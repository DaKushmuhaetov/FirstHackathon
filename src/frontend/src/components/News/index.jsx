// React
import React from 'react'

import InfoCard from './Cards/Info'
import Image from '../../images/gopniki.jpg'
// MaterialUI
import Grid from '@material-ui/core/Grid'
import Pagination from '@material-ui/lab/Pagination';

import './index.css'


import Http from '../../modules/http'

class News extends React.PureComponent {
    constructor(props) {
        super(props) 
        this.onChange(undefined, 1)
        this.state =  {
            items: [
              ]
        }
    }
    onChange = (e, page) => {
        this.setState({items: [
        {
            id: "80067854-7ac5-4721-a153-064eadbb65ab",
            title: "В Оренбурге завершены поиски без вести пропавшего пенсионера",
            description: "О том, что Андрей Боженков найден, сообщили в ПСО «ОренСпас» со ссылкой на родственников.\nНакануне, 12 июня, мужчина сам вернулся домой. С ним все в порядке. Жизни и здоровью пенсионера ничто не угрожает. В причинах его ухода из дома разбираются сотрудники полиции. Проводится проверка.",
            createDate: "0001-01-01T00:00:00",
          },
          {
            id: "ceda8745-3cfe-45da-be5c-3db04bc21d30",
            title: "В Оренбурге завершены поиски без вести пропавшего пенсионера",
            description: "О том, что Андрей Боженков найден, сообщили в ПСО «ОренСпас» со ссылкой на родственников.\nНакануне, 12 июня, мужчина сам вернулся домой. С ним все в порядке. Жизни и здоровью пенсионера ничто не угрожает. В причинах его ухода из дома разбираются сотрудники полиции. Проводится проверка.",
            createDate: "0001-01-01T00:00:00"
          },
          {
            id: "958ec663-788f-428f-bb19-6bb57ca68d1a",
            title: "В Оренбурге завершены поиски без вести пропавшего пенсионера",
            description: "О том, что Андрей Боженков найден, сообщили в ПСО «ОренСпас» со ссылкой на родственников.\nНакануне, 12 июня, мужчина сам вернулся домой. С ним все в порядке. Жизни и здоровью пенсионера ничто не угрожает. В причинах его ухода из дома разбираются сотрудники полиции. Проводится проверка.",
            createDate: "0001-01-01T00:00:00"
          }
        ]})


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
                                        date={value.createDate}
                                        image={Image}
                                        text={value.description}
                                    />
                            </Grid>    
                        )
                    })}
                </Grid>
                <Pagination className='pogination' count={10} color="primary" onChange={this.onChange}/>
            </div>
            
        )
    }
}

export default News