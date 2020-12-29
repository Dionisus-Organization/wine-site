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
                    <th scope="col" style="text-align: center"><img src="../assets/like.png" alt="Like"></th>
                </tr>
            </thead>
            <tbody>
                <router-link v-for="item in items" :key="item.id" tag="tr"
                        :to="{name: 'oneCard', params: {id: item.id, vintage: item.vintage}}">
                    <td>{{ item.wine }}</td>
                    <td><span class="wine-color-circle"><em :class="[item.color, 'color']"></em></span></td>
                    <td>{{item.wineType}}</td>
                    <td>{{item.vintage}}</td>
                    <td>{{item.country}}</td>
                    <td>{{item.score}}</td>
                    <td style="text-align: center" @click.stop>
                        <input type="checkbox" v-bind:value="item" v-model="checkedWines" @change="change">
                    </td>
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
            }
        },
        data() {
            return {
                text: 'Идет загрузка...',
                checkedWines: []
            }
        },
        components: {},
        methods: {
            change: function() {
                this.$emit('toParent', this.checkedWines);
            }
        }
    }
</script>

<style lang="scss">
    table.rates-table {
        margin: 10px 0 0 0;
        width: 100%;
        thead {
            background: $secondaryLightColor;
            color: $textColorOnColorBackground;
            th {
                vertical-align: middle;
            }
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
        img {
            width: 30px;
            height: 30px
        }
    }
</style>
