<script>
  //Importing Line class from the vue-chartjs wrapper
  import { Line, mixins } from 'vue-chartjs'
  import axios from 'axios';
  const API_URL = 'http://localhost:58881'

  //Exporting this so it can be used in other components
  export default {
    extends: Line,
    mixins: [mixins.reactiveData],
    data () {
      return {
        linedata: {},
        //Chart.js options that controls the appearance of the chart
        options: {
          scales: {
            yAxes: [{
              ticks: {
                beginAtZero: true
              },
              gridLines: {
                display: true
              }
            }],
            xAxes: [ {
              gridLines: {
                display: false
              }
            }]
          },
          legend: {
            display: true
          },
          responsive: true,
          maintainAspectRatio: false
        }
      };
    },
    created () {
      axios.get(API_URL + "/api/admin/GetChart" ,{
          params:{
          chartName: 'AverageLoggedInUsersLineChart',
        } ,
        headers: {
          token: this.userToken
        }
      }).then(response => {
          this.linedata ={
              //Data to be represented on x-axis
              labels: ['January', 'February', 'March', 'April', 'May', 'June'],
              datasets: [
                {
                  label: 'Logged in Users',
                  backgroundColor: '#455A64',
                  pointBackgroundColor: 'white',
                  borderWidth: 1,
                  pointBorderColor: '#249EBF',
                  //Data to be represented on y-axis
                  data: response.data
                }
              ]
            },
            //console.log(this.linedata)
            this.renderChart(this.linedata, this.options)
      })
    }
  }
</script>