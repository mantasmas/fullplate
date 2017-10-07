export default {
  overviewDate: new Date('2017-07-14'),
  overviewInfo: [
    {
      type: 'Soup',
      dishes: [
        {
          title: 'Šaltibarščiai',
          count: 11
        },
        {
          title: 'Meksikietiška su čili',
          count: 2
        }
      ]
    },
    {
      type: 'Main courses',
      dishes: [
        {
          title: 'Kijevo kotletas',
          count: 12
        },
        {
          title: 'Lazanija su mėsa',
          count: 1
        }
      ]
    }
  ],
  individualOrders: [
    {
      username: 'Jonas P',
      courses: {
        soup: 'Šaltibarščiai',
        mainCourse: 'Kijevo kotletas'
      }
    },
    {
      username: 'Petras J',
      courses: {
        soup: 'Šaltibarščiai',
        mainCourse: 'Lazanija su mėsa'
      }
    },
    {
      username: 'Stasys PJ',
      courses: {
        soup: '----',
        mainCourse: 'Kijevo kotletas'
      }
    }
  ]
}
