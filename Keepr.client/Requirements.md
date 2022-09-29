
<!--TODO 20 Requirements -->

[-X-] Visitors can see all keeps (login not required)

[-X-] Keep cards are displayed in accordance to mock

[-X-] Cards follow a masonry layout

[-X-] Clicking on a keep card opens the keep in a modal which adheres to mock

[-X-] All users have a public profile page

[-X-] The profile page adheres to mock


[-X-] The Vault Page shows only the keeps in that Vault

[-X-] The user should be pushed back to the home page when they attempt to open a Vault Page for a vault they do not have access to.


[-X-] On the Vault Page, users can remove keeps from their vaults


[-X-] Anytime a keep is kept in a vault the keep count is incremented


[-X-] Users can Register, login and automatically authenticated on refresh

[-X-] Create and Delete Keeps

[-X-] Anytime a keeps details are viewed, the view count is incremented

[-X-] Create and Delete Vaults

[-X-] Users can only Delete things they created

[-X-] Add keeps to vaults

[-X-] All Valid Auth API Tests pass

[-X-] All No Auth API Tests pass

[-X-] All Invalid Auth API Tests pass

[--] UI adheres to all phases of CodeWorks Design Requirements


<!-- FIXME list -->

<!-- [-X-] on vault creation, need to make the vault reactive, same for delete. -->

<!-- [-X-] check overflow cutoff, esp in log in component. -->

[--] check everything for title and aria labels. Ask what things need one.

<!-- [--] stop the flashes of old things when i load up a new appstate, look at how loading was done on magic medley.  Get a cool animation if time allows. -->

<!-- [-X-] check each page/component for silent errors. -->

[--] when refreshing on a vault page, stop the redirect to home page.

<!-- [--] select option needs a default option. -->

<!-- [--] add counter to check remaining input characters.  on the img url, maybe put a v-if warning that part of the image url may have been cut off. -->

[--] ask Jake about visually hidden for labels.

[--] check into why the router push doesn't always work.  puts it in the url but doesn't change page automatically.   ask about   import Vue from 'vue/dist/vue.js';  to fix the occasional route.push not working.

<!-- [--] Clean up logs and useless things. -->

[--] add spot for vault description.

[--] run final tests from postman.

[-X-] hide remove from vault button in vaultKeep modal from other profiles.

[--] refreshing on the vault or profile page gives the have you started your server yet error.  I thought authSettled would take care of that.

<!-- [--] Go through and ad comments to code explaining things. use NOTE -  -->