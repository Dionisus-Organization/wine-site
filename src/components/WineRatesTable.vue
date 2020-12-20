<template>
  <div>
      <table class="table table-hover rates-table">
          <thead>
          <tr>
              <th scope="col">Wine</th>
              <th scope="col">Color</th>
              <th scope="col">Wine type</th>
              <th scope="col">Vintage</th>
              <th scope="col">Country</th>
              <th scope="col">Score</th>
          </tr>
          </thead>
          <tbody>
          <router-link v-for="item in items" :key="item.id" tag="tr"
                       :to="{name: 'oneCard', params: {wine_id: item.id, vintage: item.vintage}}">
              <td>{{ item.wine }}</td>
              <td><span class="wine-color-circle"><em :class="[item.color, 'color']"></em></span></td>
              <td>{{item.wineType}}</td>
              <td>{{item.vintage}}</td>
              <td>{{item.country}}</td>
              <td>{{item.score}}</td>
          </router-link>
          </tbody>
      </table>
  </div>
</template>

<script>
    export default {
        name: "WineRatesTable",
        props: {
            items: {
                type: Array,
                required: true
            },
            user: Object
        },
        watch: {
            user: async function(newVal) {
                if (newVal) {
                    this.loggedInUser = newVal
                }
            }
        },
        data() {
            return {
                text: 'Идет загрузка...',
                isLogged: false,
                loggedInUser: this.user,
            }
        },
        components: {}
    }
</script>

<style lang="scss">
    table.rates-table {
        margin: 10px 0 0 0;
        width: 100%;
        thead {
            background: $secondaryLightColor;
            color: $textColorOnColorBackground;
        }
        tbody {
            tr:hover {
                cursor: pointer;
            }
        }
        span.wine-color-circle {
            display: block;
            width: 20px;
            height: 20px;
            border-radius: 50%;
            border: solid 2px $gray;
            padding: 3px;
            box-sizing: border-box;
            em {
                width: 10px;
                height: 10px;
                display: block;
                border-radius: 50%;
            }
            em.Red.color {
                background: $redColor;
            }
            em.White.color {
                background: $whiteColor;
            }

            em.Pink.color {
                background: $pinkColor;
            }
        }
    }
</style>
