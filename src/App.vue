<template>
    <div id="app">
        <header>
            <div class="main-header">
                <div class="site-header-container">
                    <div class="header-line"></div>
                    <div class="site-name-container">
                        <h1 class="site-name">
                            <router-link :to="{name: 'wineList'}" tag="a">
                            WINE RATING
                            </router-link>
                        </h1>
                    </div>
                    <div class="header-line"></div>
                </div>
            </div>
        </header>
        <router-view></router-view>
    </div>
</template>

<script>
    export default {
        name: 'App'
    }
</script>

<style lang="scss">
    body {
        font-family: $textFont;
        h1, h2, h3, h4, h5, h6 {
            font-family: $headerFont;
        }
        h1 {
            font-size: 1.7em;
        }
    }

    input:focus,
    select:focus,
    textarea:focus,
    button:focus {
        outline: none;
    }

    .main-header {
        background: $primaryDarkColor;
        color: $textColorOnColorBackground;
        padding: 25px 0 25px 0;
        .site-header-container {
            width: 500px;
            text-align: center;
            margin: 0 auto 0 auto;
            .header-line {
                width: 100%;
                border-top: 2px solid $textColorOnColorBackground;
            }
            .site-name-container {
                width: 480px;
                margin: 3px auto 3px auto;
                padding: 5px 0;
                border-top: 2px solid $textColorOnColorBackground;
                border-bottom: 2px solid $textColorOnColorBackground;
                h1.site-name {
                    margin: 0;
                    font-size: 64px;
                    a {
                        color: $textColorOnColorBackground;
                    }
                    a:hover {
                        text-decoration: none;
                        opacity: 0.7;
                    }
                }
            }
        }
    }

    .navigation {
        background: $primaryColor;
        color: $textColorOnColorBackground;
    }

    #nav {
    padding: 30px;

        a {
            font-weight: bold;
            color: #2c3e50;

            &.router-link-exact-active {
            color: #42b983;
            }
        }
    }
</style>
