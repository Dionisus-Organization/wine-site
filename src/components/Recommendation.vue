<template>
    <div>
        <div class="title-container">
            <h3>Recommended wines</h3>
        </div>
        <div class="container">
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
                    <tr v-for="item in items" :key="item.wine_Id">
                        <td>{{ item.wine }}</td>
                        <td><span class="wine-color-circle"><em :class="[item.color, 'color']"></em></span></td>
                        <td>{{item.wineType}}</td>
                        <td>{{item.vintage}}</td>
                        <td>{{item.country}}</td>
                        <td>{{item.score}}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script>
    export default {
        name: "Recommendation",
        props: {
            // rec_list
        },
        data() {
            return {
                items: []
            }
        },
        created() {
            // let item = {
            //     wine_Id: 1,
            //     wine: "Здесь будет ваше вино",
            // };
            // this.items.push(item);
            // this.items = this.rec_list
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
    }

    .title-container {
        text-align: center;
        background: $primaryColor;
        height: 80px;
        color: $textColorOnColorBackground;
        display: flex;
        justify-content: center;
        flex-direction: column;
    }
</style>