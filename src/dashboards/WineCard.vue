<template>
    <div>
        <div class="container" v-if="!isLoading">
            <div class="row card-info">
                <div class="col-8">
                    <h1>{{item.wine}}, {{item.vintage}}</h1>
                    <div><span class="description-field">COUNTRY: </span><span>{{item.country}}</span></div>
                    <div><span class="description-field">APPELATION: </span><span>{{item.appellation}}</span></div>
                    <div class="rate-container row">
                        <div class="col">
                            <div class="row-container">
                                <div class="description-field">GLOBAL WINE SCORE</div>
                                <div class="flex-wrapper">
                                    <div class="single-chart">
                                        <svg viewBox="0 0 36 36" class="circular-chart green">
                                            <path class="circle-bg"
                                                  d="M18 2.0845
                                                  a 15.9155 15.9155 0 0 1 0 31.831
                                                  a 15.9155 15.9155 0 0 1 0 -31.831"
                                            />
                                            <path class="circle"
                                                  @stroke-dasharray="item.score + ', 100'"
                                                  d="M18 2.0845
                                                  a 15.9155 15.9155 0 0 1 0 31.831
                                                  a 15.9155 15.9155 0 0 1 0 -31.831"
                                            />
                                            <text x="18" y="20.35" class="percentage">{{item.score}}</text>
                                        </svg>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col">
                            <div class="row-container">
                                <div class="description-field">GWS CALCULATION BASED ON</div>
                                <div><span
                                        class="score-title">{{item.journalist_count}}</span><span> Wine Critics</span>
                                </div>
                            </div>
                            <div class="row-container with-border">
                                <div class="description-field">CONFIDENCE INDEX</div>
                                <div><span class="score-title">{{item.confidence_index}}</span></div>
                                    <div class="update-date-container"><span>Updated: {{item.date }}</span></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-4">
                    <div :class="[item.color, 'wine-image']"></div>
                </div>
            </div>
        </div>
        <p v-if="isLoading">Loading...</p>
    </div>
</template>

<script>
    import api from '@/httpService';
    export default {
        name: "WineCard",
        props: ['id'],
        data() {
            return {
                isLoading: true,
                item: []
            };
        },
        created: async function () {
            await api.getById(this.id).then((data) => {
                this.isLoading = false;
                this.item = data;
            })
        },
    }
</script>

<style lang="scss">
    .card-info {
        margin: 10px 0 0 0;

        .description-field {
            color: $gray;
        }

        .rate-container {
            box-sizing: border-box;
            border: 1px solid $lightGray;
            border-radius: 3px;
            margin: 10px 0 0 0;
            position: relative;

            .col {
                padding: 0;

                .row-container {
                    padding: 10px 20px;
                }

                .row-container.with-border {
                    border-top: 1px solid $lightGray;
                }
            }

            .col:first-child {
                border-right: 1px solid $lightGray;
            }

            .score-title {
                font-size: 3em;
            }

            .update-date-container {
                color: $gray;
                font-size: 0.8em;
                bottom: 0;
                margin-bottom: 10px;
            }
        }
    }

    .wine-image {
        mask-image: url("./../assets/wineImage.svg");
        mask-repeat: no-repeat;
        mask-position: center;
        mask-size: 250px;
        background-color: $secondaryLightColor;
        width: 100%;
        height: 100%;
    }

    .wine-image.Red {
        background-color: $redColor;
    }

    .wine-image.White {
        background-color: $whiteColor;
    }

    .wine-image.Pink {
        background-color: $pinkColor;
    }

    /* Circle with rate*/
    .flex-wrapper {
        display: flex;
        flex-flow: row nowrap;
    }

    .single-chart {
        width: 60%;
        justify-content: space-around;
    }

    .circular-chart {
        display: block;
        margin: 10px auto;
        max-width: 80%;
        max-height: 250px;
    }

    .circle-bg {
        fill: none;
        stroke: #eee;
        stroke-width: 3.8;
    }

    .circle {
        fill: none;
        stroke-width: 2.8;
        stroke-linecap: round;
        animation: progress 1s ease-out forwards;
    }

    @keyframes progress {
        0% {
            stroke-dasharray: 0 100;
        }
    }

    .circular-chart.green .circle {
        stroke: #4CC790;
    }

    .percentage {
        fill: $textColor;
        font-size: 0.60em;
        text-anchor: middle;
    }

</style>
